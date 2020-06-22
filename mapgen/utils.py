import time


class ModeChangeProcedure(Exception):

	pass


class Log:

	def i(*msg):

		print(">>> INFO ({}) : {}".format(time.ctime(time.time()), *[str(i) for i in msg]));

	def d(*msg):

		print(">>> DEBUG ({}) : {}".format(time.ctime(time.time()), *[str(i) for i in msg]));

	def e(*msg):

		print(">>> ERROR ({}) : {}".format(time.ctime(time.time()), *[str(i) for i in msg]));

	def expects(variable, value):

		if variable != value:
			print(">>> EXCPECTATION ERROR {} : VARIABLE EXPECTS TO BE {} BUT IT IS {}".format(time.ctime(time.time()),
																							  value, variable))


class myrange:


	# class realised to fast work with ranges
	# useful to handlers 
	def __init__(self, value_minimum, value_maximum):

		self.value_maximum = value_maximum
		self.value_minimum = value_minimum
		self.scale_dict = {}
		for i in range(8):
			self.scale_dict[i] = (800 + i * 200, 800 + i * 200) # {0 : (800, 800), 1 : (1000, 1000) ... 7(2200, 2200)} etc. generation 


	def contains(self, num):
		if type(num) != int:
			return True
		if num >= self.value_minimum and num <= self.value_maximum:
			return True
		return False

	def get_scale_dict(self) -> dict:
		return self.scale_dict


class Coords():

	def __init__(self, x : int, y : int) -> None:

		self.x = x
		self.y = y
		self.scale_dict = myrange(None, None).get_scale_dict();

	def get_coords(self) -> tuple:
		return self.x, self.y
	def __str__(self) -> str:
		return str((self.x, self.y));


class MapCoords(Coords):

	def __init__(self, x : int, y : int) -> None:
		super().__init__(x, y);


	def toUnityCoords(self):

		pass

	def toScreenCoords(self, scale : int, map_coords : tuple):
		map_pixels_in_scale = self.scale_dict[scale][0];
		coords_regarding_to_map_zero = (int(self.x * map_pixels_in_scale / 1600), int(self.y * map_pixels_in_scale / 1600));
		coords_regarding_to_screen_zero = (map_coords[0] + coords_regarding_to_map_zero[0], map_coords[1] + coords_regarding_to_map_zero[1]);
		return ScreenCoords(coords_regarding_to_screen_zero[0], coords_regarding_to_screen_zero[1]); 

	
class ScreenCoords(Coords):

	def __init__(self, x : int, y : int) -> None:
		super().__init__(x, y);

	def toMapCoords(self, scale : int, map_coords : tuple):
		map_pixels_in_scale = self.scale_dict[scale][0];

		coords_regarding_to_map_zero_in_scale = (self.x - map_coords[0], self.y - map_coords[1]);
		coords_regarding_to_map_zero = (int(coords_regarding_to_map_zero_in_scale[0] * 1600 / map_pixels_in_scale), 
										int(coords_regarding_to_map_zero_in_scale[1] * 1600 / map_pixels_in_scale));
		return MapCoords(coords_regarding_to_map_zero[0], coords_regarding_to_map_zero[1]);

	def toUnityCoords(self, scale : int, map_coords : tuple):
		return self.toMapCoords(scale, map_coords).toUnityCoords()



class UnityCoords(Coords):
	def __init__(self, x : int, y : int) -> None:
		super().__init__(x, y);

	def toMapCoords(self) -> MapCoords:
		pass

	def toScreenCoords(self, scale : int, map_coords : tuple) -> ScreenCoords:

		return self.toMapCoords().toScreenCoords(scale, map_coords);




def toUnityCoords(coords : object) -> UnityCoords:
	try:
		return coords.toUnityCoords();
	except Exception:
		return UnityCoords(coords[0], coords[1]);

def toMapCoords(coords : object) -> MapCoords:
	try:
		return coords.toMapCoords();
	except Exception:
		return MapCoords(coords[0], coords[1])

def toScreenCoords(coords : object) -> ScreenCoords:
	try:
		return coords.toScreenCoords;
	except Exception:
		return ScreenCoords(coords[0], coords[1]);
