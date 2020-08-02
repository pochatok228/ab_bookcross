import pygame
from utils import *
import random
from code_construct import *
# =============== ОПРЕДЕЛЯТЬ ПАРАМЕТР ================
POLITICAL_MODE = 0
POPULATION_MODE = 1
PRODUCTION_MODE = 2
EDUCATION_MODE = 3
ARMY_MODE = 4
NATURAL_RESOURCES_MODE = 5
SEPARATISM_MODE = 6
CLIMATE_MODE = 7
SEA_MODE = 8
DEFENSIVE_ABILITY_MODE = 9


"""
	провинция, как минимальная географическая единица имеет 3 типа координат:

	1. Координаты на карте utils.MapCoords() целочисленные, основные
	имеют ранжирование 1600 х 1600

	2. Координаты на экране utils.ScreenCoords() целочисленные, для отрисовки
	зависят от масштаба карты и ее местоположения на экране
	ранжируются от 800х800 до 2200х2200

	3. Координаты в Unity utils.UnityCoords() вещественные, для создания объекта в Unity
	имеют ранжирование 1000х1000

"""


# ================= ОПРЕДЕЛЯТЬ ЦВЕТ ПАРАМЕТРА ==================
color_dict = {
	POLITICAL_MODE : pygame.Color(255, 255, 255),
	POPULATION_MODE : pygame.Color(255, 103, 255),
	PRODUCTION_MODE : pygame.Color(0, 255, 255),
	EDUCATION_MODE : pygame.Color(255, 255, 0),
	ARMY_MODE : pygame.Color(50, 50, 50),
	NATURAL_RESOURCES_MODE : pygame.Color(9, 90, 220),
	SEPARATISM_MODE : pygame.Color(0, 255, 0),
	CLIMATE_MODE: pygame.Color(70, 0, 5),
	SEA_MODE : pygame.Color(213, 113, 0),
	DEFENSIVE_ABILITY_MODE : pygame.Color(200, 175, 251)
};
class Province():

	def __init__(self) -> None:
		self.polygons = [];
		self.color = pygame.Color(random.randint(0, 255), 
			random.randint(0, 255), random.randint(0, 255));
		self.name : str = "";
		self.id : int = -1;
		self.capital_coords = None;
		self.connections = [] # list of ids of provinces with common borders
		self.state = None;
		self.previous_state = None;

		self.text_color = (255, 255, 0);
		self.font = pygame.font.SysFont('Century Gothic', 14);
		self.text_render = self.font.render(self.name, 1, self.color);
		self.rect_chars = self.text_render.get_rect();


 # ========== ДОБАВЛЯТЬ ПЕРЕМЕННЫЕ ДЛЯ ПАРАМЕТРОВ =================
		self.population = 100; #	 население провинции, измеряется в человеках. 
							 # 	Чем больше людей, тем дороже управление провинцией
		self.productions = 100; # 	количество производств в провинции, измеряется в штуках.
							  #		Чем больше производств, тем больше доход от провинции 
		self.education = 100;   # 	уровень образования. Целое число от 0 до 1000, хз в чем меряется
							  #		уровень образования будет давать плюшки
		self.army = 10; 		  #		 количество вояк в провинции, измеряется в человеках.
		self.natural_resources = 100; # количество природных ресурсов. Чем больше природных ресурсов,
									# тем дешевле создание производства
									#  От каждого нового производства истощаются.
		self.separatism = 50; # желание отделиться от государства 0-1000. При тысяче, провинция отделяется.
		self.climate = 100; # 0-1000 благоприятность климата
		self.sea = 0; # if value is >0 province is a part of a sea
		self.defensive_ability = 100; # значение 0 - 1000, увеличивает боеспособность солдат, обороняющих территорию




	def addPolygon(self, coords : ScreenCoords, mapsurface : pygame.Surface) -> None:
		self.polygons.append(coords.toMapCoords(mapsurface.get_scale(), mapsurface.get_coords()));
		# Log.d([str(i) for i in self.polygons]) # works +- correctly
		# Log.d("added");

	def draw(self, screen : pygame.Surface, mapsurface : pygame.Surface) -> None:
		dots : list = [coords.toScreenCoords(mapsurface.get_scale(), mapsurface.get_coords()).get_coords() for coords in self.polygons];
		if len(dots) >=3:
		# Log.d(dots)
		# pygame.draw.polygon(pygame.Surface (surface to draw), pygame.Color (color), list<tuple<int>> (dots), int (width));
			pygame.draw.polygon(screen,
				self.color,
				dots,
				5);
		elif len(dots) == 2:
			pygame.draw.line(screen,
				self.color,
				dots[0], dots[1],
				5)
		elif len(dots) == 1:
			# Log.d(type(dots[0]))

			pygame.draw.line(screen, self.color, dots[0], dots[0], 5)
		else:
			pass

		if self.capital_coords is not None:
			screen_coords = self.capital_coords.toScreenCoords(mapsurface.get_scale(), mapsurface.get_coords());
			pygame.draw.circle(screen, 
				self.color,
				(screen_coords.x, screen_coords.y), 10, 0);

	def fill_draw(self, screen, mapsurface, world,  mode : int = 0) -> None:
		dots : list = [coords.toScreenCoords(mapsurface.get_scale(), mapsurface.get_coords()).get_coords() for coords in self.polygons];
		# Log.d(mode);
		if mode == POLITICAL_MODE and self.state is not None:
			if len(dots) >=3: 
				pygame.draw.polygon(screen,
				(self.state.color.r, self.state.color.g, self.state.color.b, self.state.color.a), dots, 0);
				return None;

# ======== ДОБАВЛЯТЬ ПАРАМЕТРЫ СЮДОЙ ==============

		elif mode in [POPULATION_MODE, PRODUCTION_MODE, EDUCATION_MODE, ARMY_MODE, NATURAL_RESOURCES_MODE,
						SEPARATISM_MODE, CLIMATE_MODE, SEA_MODE, DEFENSIVE_ABILITY_MODE]:
			# Log.d("went here");
			max_paramter = max([province.get_parameter(mode) for province in world.list_of_province]);
			self.color_parameter = pygame.Color(255, 255, 255) - color_dict[mode];
			try: percent = self.get_parameter(mode) / max_paramter;
			except ZeroDivisionError: percent : float = 1.0;
			self.color_parameter = pygame.Color(int(self.color_parameter.r * percent), int(self.color_parameter.g * percent), int(self.color_parameter.b * percent));
			if len(dots) >= 3: pygame.draw.polygon(screen,
				self.color_parameter, dots, 0);
		else: pass;
		if self.capital_coords is not None:
			screen_coords = self.capital_coords.toScreenCoords(mapsurface.get_scale(), mapsurface.get_coords());
			pygame.draw.circle(screen, 
				self.color,
				(screen_coords.x, screen_coords.y), 10, 0);
		pass;




	def draw_connections(self, screen, mapsurface, world) -> None:
		for province_connected in self.connections:
			map_coords_of_final_dot = province_connected.capital_coords;
			screen_coords_of_self_capital = self.capital_coords.toScreenCoords(mapsurface.get_scale(), mapsurface.get_coords());
			screen_coords_of_other_capital = map_coords_of_final_dot.toScreenCoords(mapsurface.get_scale(), mapsurface.get_coords());
			pygame.draw.line(screen, pygame.Color("white"), screen_coords_of_self_capital.get_coords(), screen_coords_of_other_capital.get_coords(), 5);


		
# ===============ДОБАВЛЯТЬ ПАРАМЕТРЫ НА ГЕТ====================
	def get_parameter(self, mode) -> object: # in most of cases INT
		if mode == POLITICAL_MODE: return 0;
		elif mode == POPULATION_MODE: return self.population;
		elif mode == PRODUCTION_MODE: return self.productions;
		elif mode == EDUCATION_MODE: return self.education;
		elif mode == ARMY_MODE: return self.army;
		elif mode == NATURAL_RESOURCES_MODE: return self.natural_resources;
		elif mode == SEPARATISM_MODE: return self.separatism;
		elif mode == CLIMATE_MODE: return self.climate;
		elif mode == SEA_MODE: return self.sea;
		elif mode == DEFENSIVE_ABILITY_MODE : return self.defensive_ability


#===================== ДОБАВЛЯТЬ ПАРАМЕТРЫ НА СЕТ======================
	def set_parameter(self, mode : int, value : object) -> None:
		if mode == POLITICAL_MODE: return None;
		elif mode == POPULATION_MODE: self.population = value; 
		elif mode == PRODUCTION_MODE: self.productions = value; 
		elif mode == EDUCATION_MODE: self.education = value; 
		elif mode == ARMY_MODE: self.army = value; 
		elif mode == NATURAL_RESOURCES_MODE: self.natural_resources = value; 
		elif mode == SEPARATISM_MODE: self.separatism = value; 
		elif mode == CLIMATE_MODE: self.climate = value; 
		elif mode == SEA_MODE: self.sea = value; 
		elif mode == DEFENSIVE_ABILITY_MODE: self.defensive_ability = value;
		return None;


	
	
	def get_id(self) -> int: return self.id;
	def get_name(self) -> str: return self.name;
	def get_state(self) -> object: return self.state;
	def get_capital_coords(self) -> MapCoords : return self.capital_coords;
	def set_id(self, id : int) -> None: self.id = id;
	def set_name(self, name : str) -> None: self.name = name;
	def set_state(self, state : object) -> None: self.previous_state = self.state; self.state = state;
	def set_capital_coords(self, coords : MapCoords) -> None: self.capital_coords = coords;
	

	def add_connection(self, other) -> None: self.connections.append(other); # other type is also Province
	def del_connection(self) -> None: self.connections.pop();




class State():


	def __init__(self) -> None:

		self.id : int = -1;
		self.name : str = "";
		self.list_of_province = [];
		self.color = pygame.Color(random.randint(0, 255), random.randint (0, 255), random.randint(0, 255), a = 100);
		self.color.a = 100;
		# Log.d(self.color.a);

		self.political_coords = PoliticalCoords(0, 0);
		self.diplomacy_coords = DiplomacyCoords(0, 0);

		self.icon_file = Dialog().get_file();
		self.icon = pygame.sprite.Sprite();
		self.icon.image = pygame.transform.scale(pygame.image.load(self.icon_file), (50, 50));
		self.icon.rect = (375, 375, 50, 50);



	def draw(self, screen : pygame.Surface, mapsurface: pygame.Surface, world, mode : int = 0) -> None:
		for province in self.list_of_province:
			province.fill_draw(screen, mapsurface, world,  mode = mode);

	def addProvince(self, province : Province) -> None:
		if self.list_of_province == []:
			self.capital_province = province;
		self.list_of_province.append(province);
		province.set_state(self);
		# Log.d(province.get_name());

	def add_self_to_group(self, group):
		self.group = group
		self.icon.add(self.group)
		

	def update_self_political_coords(self, screen):
		self.icon.rect = (int((self.political_coords.x + 10)* 40) - 25, 
			int((self.political_coords.y + 10) * 40) - 25,
			 50, 50);

	def update_self_diplomacy_coords(self, screen):
		self.icon.rect = (int((self.diplomacy_coords.x + 10)* 40) - 25, 
			int((self.diplomacy_coords.y + 10) * 40) - 25,
			 50, 50);

	def set_id(self, id : int) -> None: self.id = id;
	def set_name(self, name : str) -> None: self.name = name;
	def get_id(self) -> int: return self.id;
	def get_name(self) -> str: return self.name;
		


class World():

	def __init__(self, name : str = "world") -> None:
		self.name = name;
		self.list_of_province = []; # <Province>
		self.list_of_states = []; # <State>
		self.province_state_dict = {};
		self.list_of_capitals = [];



	def addProvince(self, province : Province) -> None: self.list_of_province.append(province);
		# Log.d(self.list_of_province);

	def addState(self, state : State) -> None:
		self.list_of_states.append(state);


	def draw_all(self, screen : pygame.Surface, mapsurface : pygame.Surface, mode : int = POLITICAL_MODE) -> None:
		# Log.d("dra")
		

		for state in self.list_of_states:
			state.draw(screen, mapsurface, self, mode = mode);

		for province in self.list_of_province:
			province.draw(screen, mapsurface);

	def draw_borders(self, screen, mapsurface): 
		for province in self.list_of_province: province.draw(screen, mapsurface);
	
	def draw_graph(self, screen, mapsurface):
		for province in self.list_of_province:
			province.draw_connections(screen, mapsurface, self);

	def draw_parameter(self, screen, mapsurface, parameter_mode):
		# Log.d(parameter_mode);
		for province in self.list_of_province:
			province.fill_draw(screen, mapsurface, self, mode =  parameter_mode);

	def add_capital(self, coords : MapCoords, province : Province) -> None:
		self.list_of_capitals.append(coords);
		# Log.d("Me to")
		province.set_capital_coords(coords);

	def delete_province(self) -> None:
		try:
			self.list_of_capitals.pop(); 
			return self.list_of_province.pop();
		except IndexError: return None;

	def add_connection(self, capitalId1 : int, capitalId2 : int) -> None:

		province1 = self.list_of_province[capitalId1];
		province2 = self.list_of_province[capitalId2];
		province1.add_connection(province2); province2.add_connection(province1);

	def get_new_province_id(self) -> int: return len(self.list_of_province);
	def get_new_state_id(self) -> int: return len(self.list_of_states);
	def get_province(self, id : int) -> Province: return self.list_of_province[id];
	def get_state(self, id : int) -> State: return self.list_of_states[id];

	def compile_script(self):
		file_name = Dialog().get_file();


# ============== Самое интересное ======
		try:
			with open(file_name, 'w') as file:
				file.write(Construction(self))
		except Exception:
			pass;