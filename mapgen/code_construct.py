from utils import *
from triangulation import *


def Construction(world):
	ta = []
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
		ta.append('        province_{}.name = "province_{}";'.format(i, i))
		ta.append('        province_{}_provincegen = province_{}.GetComponent<provincegen>();'.format(i, i))
		ta.append('        province_{}_provincegen.name = "{}";'.format(i, province.name))
		"""
		dots_string_begin = '        province_' + str (i) + '_provincegen.dots = new List<Vector3> {'
		dots_string_end = '};'
		dots_string_plot = ', '.join(["new Vector3({}, 0, {})".format(str(mc.toUnityCoords().x) + 'f', str(mc.toUnityCoords().y) + 'f') for mc in province.polygons])
		"""

		province_verticles, province_triangles = triangulate(province.polygons);
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


