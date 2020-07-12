import time
import tkinter
from tkinter import filedialog

class ModeChangeProcedure(Exception):

	pass


class Log:

	def i(*msg):

		print(">>> INFO ({}) : {}".format(time.ctime(time.time()), ', '.join([str(i) for i in msg])));

	def d(*msg):

		print(">>> DEBUG ({}) : {}".format(time.ctime(time.time()), ', '.join([str(i) for i in msg])));

	def e(*msg):

		print(">>> ERROR ({}) : {}".format(time.ctime(time.time()), ', '.join([str(i) for i in msg])));

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
		for i in range(12):
			self.scale_dict[i] = (800 + i * 200, 800 + i * 200) # {0 : (800, 800), 1 : (1000, 1000) ... 7(2200, 2200)} etc. generation 


	def contains(self, num):
		if type(num) != int:
			return True
		if num >= self.value_minimum and num <= self.value_maximum:
			return True
		return False

	def get_scale_dict(self) -> dict:
		return self.scale_dict

	def get_distance(self, coords1 : object, coords2 : object) -> float:
		try: x1 = coords1.x; y1 = coords1.y;
		except Exception: x1 = coords1[0]; y1 = coords1[1];
		try: x2 = coords2.x; y2 = coords2.y;
		except Exception: x2 = coords2[0], y2 = coords2[1];
		a = (x1 - x2) ** 2;
		b = (y1 - y2) ** 2;
		c = (a + b) ** 0.5;
		return c;


class Coords():

	def __init__(self, x : int, y : int) -> None:

		self.x = x
		self.y = y
		self.scale_dict = myrange(None, None).get_scale_dict();

	def get_coords(self) -> tuple:
		return self.x, self.y
	def __str__(self) -> str:
		return str((self.x, self.y));


class PoliticalCoords(Coords):

	def __init__(self, x : float, y : float) -> None:
		if myrange(-10, 10).contains(x) and myrange(-10, 10).contains(y): super().__init__(x, y);
		else: raise TypeException; del self;
		self.x = x;
		self.y = y;


class DiplomacyCoords(Coords):

	def __init__(self, x : float, y : float) -> None:
		if myrange(-10, 10).contains(x) and myrange(-10, 10).contains(y): super().__init__(x, y);
		else: raise TypeException; del self;
		self.x = x; self.y = y;
		

	# def get_distance(self, other) would be realised in C#

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



class Dialog:

	def __init__(self) -> None:
		self.value = "";

	def get(self, message : str = "") -> object:
		self.get_value(message = message);
		return self.value;

	def answer_and_quit(self) -> None:
		self.value = self.value_enterfield.get();
		self.dialog_window.destroy();
		return self.value;  


	def get_value(self, message : str  = "") -> object:
		self.dialog_window = tkinter.Tk();
		self.dialog_window.title(message);
		self.dialog_window.geometry('300x75+200+100');
		# self.dialog_window.geomerty("300x250");
		self.value_enterfield = tkinter.Entry();
		
		self.text_label = tkinter.Label(self.dialog_window,
								text = message, 
								);
		self.get_value_button = tkinter.Button(self.dialog_window, text = "OK",
			font = 16, command = self.answer_and_quit);

		self.text_label.pack();
		self.value_enterfield.pack();
		self.get_value_button.pack();

		self.dialog_window.mainloop();

	def get_file(self) -> str: #file name
		self.root = tkinter.Tk(); self.root.withdraw();
		self.filename = filedialog.askopenfilename();
		self.root.destroy();
		return self.filename

	


