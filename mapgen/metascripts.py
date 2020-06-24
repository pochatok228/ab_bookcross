import pygame
from utils import *
import random



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
		self.capital_coords = None;
		self.connections = [] # list of ids of provinces with common borders


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


		

	def set_name(self, name : str) -> None:
		self.name = name;

	def set_capital_coords(self, coords : MapCoords) -> None:
		self.capital_coords = coords;

	def get_id(self, world : object) -> int:
		return world.list_of_province.index(self);




class State():

	pass



class World():

	def __init__(self, name : str = "world") -> None:
		self.name = name;
		self.list_of_province = []; # <Province>
		self.list_of_states = []; # <State>
		self.province_state_dict = {};
		self.list_of_capitals = [];


	def addProvince(self, province : Province) -> None:
		self.list_of_province.append(province);
		# Log.d(self.list_of_province);


	def draw_all(self, screen : pygame.Surface, mapsurface : pygame.Surface) -> None:
		# Log.d("dra")
		for province in self.list_of_province:

			province.draw(screen, mapsurface);

	def add_capital(self, coords : MapCoords, province : Province) -> None:
		self.list_of_capitals.append(coords);
		# Log.d("Me to")
		province.set_capital_coords(coords);

	def delete_province(self) -> None:
		return self.list_of_province.pop();