import sys
import pygame


from utils import Log, myrange
from utils import ModeChangeProcedure # logging and range classes
from utils import ScreenCoords, MapCoords, UnityCoords # descritprion in the beginning of metascripts.py
from utils import toScreenCoords, toMapCoords, toUnityCoords 
from utils import Dialog
from handlers import Map
from handlers import MenuBlock # right and bottom menu blocks
from handlers import Button

from metascripts import Province, State, World;
from metascripts import POLITICAL_MODE;





optimized_myrange = myrange(None, None);
current_version : str = "0.0.1.8";
resolution : tuple = (1000, 900);
mapsurface : pygame.Surface = None;
mapgroup : pygame.sprite.Group = None;
map_x, map_y  = 0, 0;
scale : int = None;
screen : pygame.Surface = None;




def activate_buttons(button_group : pygame.sprite.Group) -> None:
	for button in button_group:
		button.activate();


def deactivate_buttons(button_group : pygame.sprite.Group) -> None:
	for button in button_group:
		button.deactivate();


def check_buttons(events : list, 
	button_group : pygame.sprite.Group,
	mode : int) -> Exception:
	expectable_button = None
	for event in events:
		if event.type == pygame.MOUSEBUTTONDOWN and event.button == 1:
			for button in button_group:
				if button.collides(event.pos):
					expectable_button = button
					break
			if expectable_button is not None and expectable_button.is_able_to_press():
				pressed_button = expectable_button
				if pressed_button.text == 'Delete Province':
					# Log.d("delete_province button was pressed")
					world.delete_province();
					return 0;
				if pressed_button.mode != mode:
					mode = pressed_button.mode;
					deactivate_buttons(button_group);
					pressed_button.press();
				
				else:
					mode = 0;
					activate_buttons(button_group);	
	return mode

def check_capitals(world: World, event_click : tuple, mapsurface : pygame.Surface) -> int: # id of province, whos capital has been pressed
	event_click_screen_coords = ScreenCoords(event_click[0], event_click[1]);
	event_click_map_coords = event_click_screen_coords.toMapCoords(mapsurface.get_scale(), mapsurface.get_coords());
	for capital_id in range(len(world.list_of_capitals)):
		if optimized_myrange.get_distance(world.list_of_capitals[capital_id], event_click_map_coords) <= 20:
			return capital_id;

def MapMovementAndZoom(events : list, 
	pressed : bool,
	last_coords : tuple) -> None:
	global mapgroup;
	if pressed:
		up_coords = pygame.mouse.get_pos()
		delta = (up_coords[0] - last_coords[0], up_coords[1] - last_coords[1])
		Map.move(mapsurface, delta)
		last_coords = up_coords
	for event in events:
		if event.type == pygame.MOUSEBUTTONDOWN and event.button == 3 and mapsurface.collides(event.pos):
			pressed = True
			last_coords = pygame.mouse.get_pos();
			# Log.d("pressed", pressed)

		if event.type == pygame.MOUSEBUTTONUP and event.button == 3:
			pressed = False

		if event.type == pygame.MOUSEBUTTONDOWN and event.button == 4 and mapsurface.collides(event.pos):

			# ZOOM +
			# Log.d("I`ve catch this event!")
			# Log.d("mapsurface.scale = {}".format(mapsurface.scale))
			if myrange(0, 6).contains(mapsurface.scale):
				mapsurface.set_scale(mapsurface.scale + 1)
				mapgroup.update()

		if event.type == pygame.MOUSEBUTTONDOWN and event.button == 5 and mapsurface.collides(event.pos):

			# ZOOM -
			if myrange(1, 7).contains(mapsurface.scale):
				mapsurface.set_scale(mapsurface.scale - 1)
				mapgroup.update()



		if event.type == pygame.QUIT:
			pygame.quit();
			sys.exit();
	return pressed, last_coords


def provinceGeometryEdit(events : list, province : Province,  mapsurface : pygame.Surface) -> None:
	for event in events:
		if event.type == pygame.MOUSEBUTTONDOWN:
			if event.button == 1 and mapsurface.collides(event.pos): #editing borders
				province.addPolygon(toScreenCoords(event.pos), mapsurface);
			elif event.button == 2 and mapsurface.collides(event.pos): #editing capitals
				# Log.d("Got it");
				capital_coords = event.pos;
				map_capital_coords = toScreenCoords(capital_coords).toMapCoords(mapsurface.get_scale(), 
													mapsurface.get_coords());
				world.add_capital(map_capital_coords, province);
			
def addStateMode(events : list,
	state : State, mapsurface : pygame.Surface) -> None:
	for event in events:
		if event.type == pygame.MOUSEBUTTONDOWN:
			if event.button == 1 and mapsurface.collides(event.pos):
				variable_click = event.pos;
				variable_capital_id = check_capitals(world, variable_click, mapsurface);
				if variable_capital_id is not None and world.get_province(variable_capital_id).get_state() != state:
					state.addProvince(world.get_province(variable_capital_id));





def main(world : World, mapfile : str = "skyrim_map.jpg") -> int:

	global mapsurface, scale, screen, mapgroup

	pygame.init();
	pygame.display.set_caption("MapGen {}".format(current_version));
	screen = pygame.display.set_mode(resolution);

	# Log.d(type(screen));
	# main map initialization
	mapgroup = pygame.sprite.Group();
	mapsurface = Map(mapfile, 0, 0, 0);
	mapsurface.add(mapgroup);

	# button blocks drawing

	menu_blocks_group = pygame.sprite.Group();
	right_menu_block = MenuBlock(800, 0, 200, 800);
	bottom_menu_block = MenuBlock(0, 800, 1000, 100)
	menu_blocks_group.add(right_menu_block);
	menu_blocks_group.add(bottom_menu_block);



	Log.expects(menu_blocks_group.has(right_menu_block), True)
	Log.expects(menu_blocks_group.has(bottom_menu_block), True)
	Log.expects(mapgroup.has(mapsurface), True)

	right_button_group = pygame.sprite.Group();
	add_province_button = Button(x = 825, y = 25, w = 150, h = 75, 
									text = "Add province", mode = 1);
	delete_province_button = Button(x = 825, y = 125, w = 150, h = 75,
		text = "Delete Province", mode = 0);
	add_state_button = Button(
		x = 825, y = 225,
		w = 150, h = 75, text = "Add State", mode = 2);

	right_button_group.add(add_province_button);
	right_button_group.add(delete_province_button);
	right_button_group.add(add_state_button);




	scale = 0 # масштаб карты. транслятор масштаб : размер карты в mapsurface.scale_dict
	pressed = False
	mode = 0;
	last_coords = None;
	current_added_province = None;

	"""
		Mode 0: 	is the mode with political map showing and nothinhg else
		Mode 1: 	mode when user adds province drawing polygons
		Mode 2: 	mode when user color provinces into countries
		Mode 3: 	mode when user draw a graph
	"""


	# main screen cycle
	while True:

		screen.fill((228, 228, 228))
		Log.expects(myrange(0, 2).contains(mode), True);

		if myrange(0, 2).contains(mode):

			mapgroup.draw(screen);
			world.draw_all(screen, mapsurface);
		
		


		"""
		В отдельный этап вынесены эвенты со сменой режима работы"
		"""

		frameEventsList = pygame.event.get();

		if mode == 0:
			pressed, last_coords = MapMovementAndZoom(frameEventsList, pressed, last_coords);
			try:
				new_mode : int = check_buttons(frameEventsList, right_button_group, mode);
				if mode != new_mode : mode = new_mode; raise ModeChangeProcedure;
			except ModeChangeProcedure:
				if mode == 1:
					province_name = Dialog().get(message = "Введите название провинции");
					current_added_province = Province();
					current_added_province.set_id(world.get_new_province_id());
					current_added_province.set_name(province_name);
					world.addProvince(current_added_province);
				elif mode == 2:
					state_name = Dialog().get(message = "Введите название страны");
					current_added_state = State();
					current_added_state.set_id(world.get_new_state_id());
					current_added_state.set_name(state_name);
					world.addState(current_added_state);

		elif mode == 1:
			pressed, last_coords = MapMovementAndZoom(frameEventsList, pressed, last_coords);
			try:
				new_mode : int = check_buttons(frameEventsList, right_button_group, mode);
				if mode != new_mode : mode = new_mode; raise ModeChangeProcedure;
			except ModeChangeProcedure:
				if mode == 0:
					current_added_province = None;
					if len(world.list_of_capitals) != len(world.list_of_province): world.list_of_province.pop();
			provinceGeometryEdit(frameEventsList,
								current_added_province, 
								mapsurface);

		elif mode == 2:
			pressed, last_coords = MapMovementAndZoom(frameEventsList, pressed, last_coords);
			try:
				new_mode : int = check_buttons(frameEventsList, right_button_group, mode);
				if mode != new_mode : mode = new_mode; raise ModeChangeProcedure;
			except ModeChangeProcedure:
				if mode == 0:
					current_added_state = None;

			addStateMode(frameEventsList, current_added_state, mapsurface);




		menu_blocks_group.draw(screen); # блоки меню отрисовываются при любом режиме работы программы
		right_button_group.draw(screen); # кнопки отрисовываются при любом режиме работы программы.	
		pygame.display.flip()

	return 0;



if __name__ == '__main__':

	# консольная конфигурация мира типа введите название

	# Расскоментировать для включения диалогового окна

	# name = Dialog().get(message = "Введите имя карты")
	mapfile = Dialog().get_file();
	try: world = World(name = name); 
	except Exception: world = World();

	main(world, mapfile = mapfile);