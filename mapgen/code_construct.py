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
			try:
				triangle_normal.append(verticles_copy.index(triangle_verticle));
			except Exception as e:
				Log.d(triangle_verticle)
				Log.d(verticles_copy)
				Log.d(e)
		triangles[i] = tuple(triangle_normal);
	# Log.d(triangles)
	return verticles_copy, triangles





def Construction(world):
	ta = [];
	ta.append("using System.Collections; using System.Collections.Generic; using UnityEngine;")
	ta.append("")
	ta.append("public class mapgen : MonoBehaviour {")
	ta.append("    public GameObject province_template;")
	for i in range(len(world.list_of_province)):
		ta.append("    private GameObject province_{};".format(i))
		ta.append("    private provincegen province_{}_provincegen;".format(i))

	ta.append("")
	ta.append("    void Start()")
	ta.append("    {")
	for i in range(len(world.list_of_province)):
		province = world.list_of_province[i];
		ta.append("        province_{} = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;".format(i))
		# ta.append('        province_{}.name = "province_{}";'.format(i, i))
		ta.append('        province_{}_provincegen = province_{}.GetComponent<provincegen>();'.format(i, i))
		ta.append('        province_{}_provincegen.name = "province_{}";'.format(i, i));
		ta.append('        province_{}_provincegen.province_name = "{}";'.format(i, province.name));
		province_verticles, province_triangles = triangulation_function(province)
		# Log.d(province_verticles)
		dots_string_begin = '        province_' + str (i) + '_provincegen.verticles = new Vector3[] {'
		dots_string_end = '};'
		dots_string_plot = ', '.join(["new Vector3({}f, 0, {}f)".format(10 - coords[0], coords[1]) for coords in province_verticles])
		# Log.d(dots_string_plot)
		ta.append(dots_string_begin + dots_string_plot + dots_string_end)
		tr_b = "		province_" + str(i) + "_provincegen.triangles = new int[] {"
		tr_p = ", ".join(["{}, {}, {}".format(i[0], i[1], i[2]) for i in province_triangles])
		tr_e = "};"
		ta.append(tr_b + tr_p + tr_e)
		try:
			ta.append('        province_{}_provincegen.state_color = new Color({}, {}, {});'.format(i, province.state.color.r, province.state.color.g, province.state.color.b));
		except Exception:
			ta.append('        province_{}_provincegen.state_color = new Color(255, 255, 255);'.format(i));
		ta.append('        province_{}_provincegen.productions = {};'.format(i, province.productions))
		ta.append('        province_{}_provincegen.education = {};'.format(i, province.education))
		ta.append('        province_{}_provincegen.army= {};'.format(i, province.army))
		ta.append('        province_{}_provincegen.natural_resources= {};'.format(i, province.natural_resources))
		ta.append('        province_{}_provincegen.separatism= {};'.format(i, province.separatism))
		ta.append('        province_{}_provincegen.climate = {};'.format(i, province.climate))
		ta.append('        province_{}_provincegen.sea = {};'.format(i, province.sea))
		ta.append('        province_{}_provincegen.defensive_ability = {};'.format(i, province.defensive_ability))
		ta.append('');
	ta.append('    }')
	ta.append('}')

	return '\n'.join(ta);


