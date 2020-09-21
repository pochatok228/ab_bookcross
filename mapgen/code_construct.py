from utils import *
import triangulation


def triangulation_function(province):

	verticles : list = []; triangles : list = [];

	polygons = [coords.toUnityCoords() for coords in province.polygons]
	# print(polygons)
	verticles = [(coords.x, coords.y) for coords in polygons]
	Log.d(verticles)
	verticles = verticles[::-1] if triangulation.IsClockwise(verticles) else verticles[:];
	verticles_copy = verticles.copy()

	while len(verticles) >= 3:
		ear = triangulation.GetEar(verticles)
		if ear == []:
			break
		triangles.append(ear)

	for i in range(len(triangles)):
		triangle = triangles[i]
		triangle_normal  = []
		for triangle_verticle in triangle: 
			triangle_normal.append(verticles_copy.index(triangle_verticle));
		triangles[i] = tuple(triangle_normal);
	# Log.d(triangles)
	return verticles_copy, triangles



def Construction(world):
	ta = [];
	ta.append("using System.Collections; using System.Collections.Generic; using UnityEngine;")
	ta.append("")
	ta.append("public class mapgen : MonoBehaviour {")
	ta.append("    public GameObject province_template;")
	ta.append("    public GameObject state_template;")
	for i in range(len(world.list_of_province)):
		ta.append("    private GameObject province_{};".format(i))
		ta.append("    private provincegen province_{}_provincegen;".format(i))
	for i in range(2): ta.append("");
	for i in range(len(world.list_of_states)):
		ta.append("    private GameObject state_{};".format(i))
		ta.append("    private stategen state_{}_stategen;".format(i))

	ta.append("")
	ta.append("    void Awake()")
	ta.append("    {")
	for i in range(len(world.list_of_province)):
		province = world.list_of_province[i];
		p_id = province.get_id();
		ta.append("        province_{} = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;".format(p_id))
		# ta.append('        province_{}.name = "province_{}";'.format(i, i))
		ta.append('        province_{}_provincegen = province_{}.GetComponent<provincegen>();'.format(p_id, p_id))
		ta.append('        province_{}_provincegen.name = "province_{}";'.format(p_id, p_id));
		ta.append('        province_{}_provincegen.province_name = "{}";'.format(p_id, str(province.get_name())));
		province_verticles, province_triangles = triangulation_function(province)
		# Log.d(province_verticles)
		dots_string_begin = '        province_' + str (p_id) + '_provincegen.verticles = new Vector3[] {'
		dots_string_end = '};'
		dots_string_plot = ', '.join(["new Vector3({}f, 0, {}f)".format(10 - coords[0], coords[1]) for coords in province_verticles])
		# Log.d(dots_string_plot)
		ta.append(dots_string_begin + dots_string_plot + dots_string_end)
		tr_b = "		province_" + str(i) + "_provincegen.triangles = new int[] {"
		tr_p = ", ".join(["{}, {}, {}".format(i[0], i[1], i[2]) for i in province_triangles])
		tr_e = "};"
		ta.append(tr_b + tr_p + tr_e)
		capital_coords_unity = province.capital_coords.toUnityCoords(); capital_coords_unity.x *= -1;
		ta.append("		province_{}_provincegen.capital_coords = new Vector3({}f , 0, {}f);".format(p_id, 10 + capital_coords_unity.x, capital_coords_unity.y))
		try:
			ta.append('        province_{}_provincegen.state_color = new Color({}, {}, {});'.format(p_id, province.state.color.r, province.state.color.g, province.state.color.b));
		except Exception:
			ta.append('        province_{}_provincegen.state_color = new Color(255, 255, 255);'.format(i));
		ta.append('        province_{}_provincegen.productions = {};'.format(p_id, province.productions))
		ta.append('        province_{}_provincegen.education = {};'.format(p_id, province.education))
		ta.append('        province_{}_provincegen.army= {};'.format(p_id, province.army))
		ta.append('        province_{}_provincegen.natural_resources= {};'.format(p_id, province.natural_resources))
		ta.append('        province_{}_provincegen.separatism= {};'.format(p_id, province.separatism))
		ta.append('        province_{}_provincegen.climate = {};'.format(p_id, province.climate))
		ta.append('        province_{}_provincegen.sea = {};'.format(p_id, province.sea))
		ta.append('        province_{}_provincegen.defensive_ability = {};'.format(p_id, province.defensive_ability))
		for connection in province.connections:
			ta.append('        province_{}_provincegen.AddConnection({});'.format(p_id, connection.get_id()));
		ta.append("        province_{}_provincegen.Construct();".format(p_id));
		ta.append('');
	for i in range(4): ta.append('');
	for i in range(len(world.list_of_states)):
		state = world.list_of_states[i]
		s_id = state.get_id();
		ta.append("         state_{} = Instantiate(state_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;".format(s_id))
		ta.append('			state_{}.name = "state_{}";'.format(s_id, s_id));
		ta.append("			state_{}_stategen = state_{}.GetComponent<stategen>();".format(s_id, s_id))
		ta.append('			state_{}_stategen.state_name = "{}";'.format(s_id, state.get_name()));
		cr, cg, cb = state.color.r, state.color.g, state.color.b;
		ta.append("			state_{}_stategen.state_color = new Color({}, {}, {});".format(s_id, cr, cg, cb));
		ta.append("         state_{}_stategen.political_coords = new Vector2({}f, {}f);".format(s_id, state.political_coords.x, state.political_coords.y));
		ta.append("         state_{}_stategen.diplomacy_coords = new Vector2({}f, {}f);".format(s_id, state.diplomacy_coords.x, state.diplomacy_coords.y));
		for province in state.list_of_province:
			ta.append("         state_{}_stategen.AddProvince({});".format(s_id, province.get_id()))
		IconUnityName = '.'.join(state.icon_file.split('/')[-1].split('.')[:-1]);
		ta.append('		   state_{}_stategen.icon_file = "{}";'.format(s_id, IconUnityName))
		ta.append('		   state_{}_stategen.state_description = "";'.format(s_id));
		ta.append('        state_{}_stategen.capital_province = GameObject.Find("province_{}");'.format(s_id, state.capital_province.get_id()));
		ta.append('')
		


	ta.append('    }')
	ta.append('}')

	return '\n'.join(ta);