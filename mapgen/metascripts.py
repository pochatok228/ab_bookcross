import pygame
from utils import *
import random

POLITICAL_MODE = 0


"""
	провинция, как минимальная географическая единица имеет 3 типа координат:

	1. Координаты на карте utils.MapCoords() целочисленные, основные
	имеют ранжирование 1600 х 1600

	2. Координаты на экране utils.ScreenCoords() целочисленные, для отрисовки
	зависят от масштаба карты и ее местоположения на экране
	ранжируются от 800х800 до 2200х2200

	3. Координаты в Unity utils.UnityCoords() вещественные, для создания объекта в Unity
	имеют ранжирование 10х10

"""
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

	def fill_draw(self, screen, mapsurface, mode : int = 0):
		dots : list = [coords.toScreenCoords(mapsurface.get_scale(), mapsurface.get_coords()).get_coords() for coords in self.polygons];
		if len(dots) >=3: pygame.draw.polygon(screen,
			(self.state.color.r, self.state.color.g, self.state.color.b, self.state.color.a), dots, 0);
		else: pass;

	def draw_connections(self, screen, mapsurface, world) -> None:
		for province_connected in self.connections:
			map_coords_of_final_dot = province_connected.capital_coords;
			screen_coords_of_self_capital = self.capital_coords.toScreenCoords(mapsurface.get_scale(), mapsurface.get_coords());
			screen_coords_of_other_capital = map_coords_of_final_dot.toScreenCoords(mapsurface.get_scale(), mapsurface.get_coords());
			pygame.draw.line(screen, pygame.Color("white"), screen_coords_of_self_capital.get_coords(), screen_coords_of_other_capital.get_coords(), 5);


	
	
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


	def draw(self, screen : pygame.Surface, mapsurface: pygame.Surface) -> None:
		for province in self.list_of_province:
			province.fill_draw(screen, mapsurface, mode = POLITICAL_MODE);

	def addProvince(self, province : Province) -> None:
		self.list_of_province.append(province);
		province.set_state(self);
		Log.d(province.get_name());

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
		# в случае с world за счет того, что у класса лишь один эземпляр
		# функции типа get_parameter() не буду прописывать.


	def addProvince(self, province : Province) -> None: self.list_of_province.append(province);
		# Log.d(self.list_of_province);

	def addState(self, state : State) -> None:
		self.list_of_states.append(state);


	def draw_all(self, screen : pygame.Surface, mapsurface : pygame.Surface, mode = POLITICAL_MODE) -> None:
		# Log.d("dra")
		

		for state in self.list_of_states:
			state.draw(screen, mapsurface);

		for province in self.list_of_province:
			province.draw(screen, mapsurface);

	def draw_graph(self, screen, mapsurface):
		for province in self.list_of_province:
			province.draw_connections(screen, mapsurface, self);

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

