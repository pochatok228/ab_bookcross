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
from metascripts import POLITICAL_MODE, POPULATION_MODE





optimized_myrange = myrange(None, None);
current_version : str = "0.0.2.2";
resolution : tuple = (1000, 900);
mapsurface : pygame.Surface = None;
mapgroup : pygame.sprite.Group = None;
map_x, map_y  = 0, 0;
scale : int = None;
screen : pygame.Surface = None;
bottom_button_group : object = None;




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
			# Log.expects(expectable_button, None);
			if expectable_button is not None and expectable_button.is_able_to_press():
				pressed_button = expectable_button;
				# Log.d(pressed_button.text);
				if pressed_button.text == 'Delete Province':
					# Log.d("delete_province button was pressed")
					world.delete_province();
					return 0;
				Log.expects(pressed_button.mode, mode);
				if pressed_button.mode != mode:

					mode = pressed_button.mode;
					Log.d(mode);
					deactivate_buttons(button_group);
					pressed_button.press();
					return mode;
				
				else:
					mode = 0;
					activate_buttons(button_group);	
	# Log.d(mode);
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
			if myrange(0, 10).contains(mapsurface.scale):
				mapsurface.set_scale(mapsurface.scale + 1)
				mapgroup.update()

		if event.type == pygame.MOUSEBUTTONDOWN and event.button == 5 and mapsurface.collides(event.pos):

			# ZOOM -
			if myrange(1, 11).contains(mapsurface.scale):
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
				if len(world.list_of_capitals) != len(world.list_of_province): world.add_capital(map_capital_coords, province);
				else: province.set_capital_coords(map_capital_coords); world.list_of_capitals[-1] = map_capital_coords;

						
def addStateMode(events : list,
	state : State, mapsurface : pygame.Surface) -> None:
	for event in events:
		if event.type == pygame.MOUSEBUTTONDOWN:
			if event.button == 1 and mapsurface.collides(event.pos):
				variable_click = event.pos;
				variable_capital_id = check_capitals(world, variable_click, mapsurface);
				# Log.d("got ok")
				# try: Log.d(world.get_province(variable_capital_id).get_state().name);
				# except Exception : Log.d('no name');
				if variable_capital_id is not None and world.get_province(variable_capital_id).get_state() is not state:
					state.addProvince(world.get_province(variable_capital_id));
		if event.type == pygame.KEYDOWN:
			if event.key in map(ord, list('ZzЯя')):
				# Log.i("Caught");
				try: province = state.list_of_province.pop(); province.state = province.previous_state;
				except Exception: pass;


def graphEditMode(events : list, screen, mapsurface : pygame.Surface, world : World, capital_pressed : int) -> int:
	# Log.e(type(capital_pressed));
	if capital_pressed == -1:
		for event in events:
			if event.type == pygame.MOUSEBUTTONDOWN:
				if event.button == 1:
					variable_capital_id = check_capitals(world, event.pos, mapsurface);
					if variable_capital_id is not None:
						capital_pressed = variable_capital_id;
						return capital_pressed;
		return -1;
	else:
		# для начала рисуем линию между стартом и мышкой
		mouse_screen_coords = pygame.mouse.get_pos();
		capital_screen_coords = world.list_of_capitals[capital_pressed].toScreenCoords(mapsurface.get_scale(), mapsurface.get_coords());
		# Log.d(capital_screen_coords);
		pygame.draw.line(screen, pygame.Color("white"), capital_screen_coords.get_coords(), mouse_screen_coords, 5);
		for event in events:
			if event.type == pygame.MOUSEBUTTONDOWN:
				if event.button == 1:
					variable_capital_id = check_capitals(world, event.pos, mapsurface);
					if variable_capital_id is not None:
						world.add_connection(capital_pressed,variable_capital_id);
						return -1;



		return capital_pressed;


def editParameterMode(events : list, screen, mapsurface, world, parameter_mode):

	if parameter_mode == POLITICAL_MODE:
		# Log.d([i for i in bottom_button_group]);
		parameter_mode = check_buttons(events, bottom_button_group, parameter_mode);
		# Log.d(parameter_mode);
	else:
		for event in events:
			if event.type == pygame.MOUSEBUTTONDOWN:
				if mapsurface.collides(event.pos):
					variable_capital_id = check_capitals(world, event.pos, mapsurface);
					if variable_capital_id is not None:
						province = world.get_province(variable_capital_id);
						value_of_parameter = province.get_parameter(parameter_mode);
						if event.button == 1:
							if value_of_parameter < 100: value_of_parameter += 10;
							else: value_of_parameter = int(value_of_parameter * 1.1);
						elif event.button == 2:
							if value_of_parameter < 100: value_of_parameter -= 10;
							else:  value_of_parameter = int(value_of_parameter * 0.9);
							if value_of_parameter < 0: value_of_parameter = 0;
						province.set_parameter(parameter_mode, value_of_parameter);
				else:
					parameter_mode = check_buttons(events, bottom_button_group, parameter_mode);
	if parameter_mode is None: return 0;
	return parameter_mode;




def main(world : World, mapfile : str = "skyrim_map.jpg") -> int:

	global mapsurface, scale, screen, mapgroup, bottom_button_group;

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



	# Log.expects(menu_blocks_group.has(right_menu_block), True)
	# Log.expects(menu_blocks_group.has(bottom_menu_block), True)
	# Log.expects(mapgroup.has(mapsurface), True)

	right_button_group = pygame.sprite.Group();
	add_province_button = Button(x = 825, y = 25, w = 150, h = 75, 
									text = "Add Province", mode = 1);
	delete_province_button = Button(x = 825, y = 125, w = 150, h = 75,
		text = "Delete Province", mode = 0);
	add_state_button = Button(
		x = 825, y = 225,
		w = 150, h = 75, text = "Add State", mode = 2);
	edit_graph_button = Button(
		x = 825, y = 325,
		w = 150, h = 75, text = "Edit Graph", mode = 3);
	edit_parameter_button = Button(
		x = 825, y = 425, w = 150, h = 75, text = "Edit Parameter", mode = 4);

	right_button_group.add(add_province_button);
	right_button_group.add(delete_province_button);
	right_button_group.add(add_state_button);
	right_button_group.add(edit_graph_button);
	right_button_group.add(edit_parameter_button);


	bottom_button_group = pygame.sprite.Group();

	political_parameter_button = Button(x = 10, y = 805, w = 180, h = 40, text = "Political", mode = POLITICAL_MODE);
	population_parameter_button = Button(x = 10, y = 855, w = 180, h = 40, text = "Population", mode = POPULATION_MODE);

	bottom_button_group.add(political_parameter_button);
	bottom_button_group.add(population_parameter_button);



	scale = 0 # масштаб карты. транслятор масштаб : размер карты в mapsurface.scale_dict
	pressed = False; capital_pressed = -1;
	mode = 0;
	last_coords = None;
	current_added_province = None;
	text_showing : bool = True; icon_showing : bool = True;
	parameter_mode : int = POLITICAL_MODE;
	Log.d(parameter_mode);

	"""
		Mode 0: 	is the mode with political map showing and nothinhg else
		Mode 1: 	mode when user adds province drawing polygons
		Mode 2: 	mode when user color provinces into countries
		Mode 3: 	mode when user draw a graph
		Mode 4:		mode when user is editing a province parameters
	"""


	# main screen cycle
	while True:
		frameEventsList = pygame.event.get();

		# обработка вспомогательных модов работы

		for event in frameEventsList:
			if event.type == pygame.KEYDOWN:
				try:
					if event.key in map(ord, list('тТnN')):
						text_showing = not text_showing;
						# Log.d('got it');
						continue
					elif event.key in map(ord, list('iIшШ')):
						icon_showing = not icon_showing;

				except Exception:
					pass

		screen.fill((228, 228, 228))
		# Log.expects(myrange(0, 2).contains(mode), True);

		if myrange(0, 4).contains(mode):
			pressed, last_coords = MapMovementAndZoom(frameEventsList, pressed, last_coords);
			mapgroup.draw(screen);
			if myrange(0, 3).contains(mode): world.draw_all(screen, mapsurface, mode = parameter_mode);
			if mode == 3 : world.draw_graph(screen, mapsurface);
			if mode == 4: world.draw_borders(screen, mapsurface);
		
		


		"""
		В отдельный этап вынесены эвенты со сменой режима работы"
		"""

		

		if mode == 0: # mode enter
			
			try:
				new_mode : int = check_buttons(frameEventsList, right_button_group, mode);
				if mode != new_mode : mode = new_mode; raise ModeChangeProcedure;
			except ModeChangeProcedure:
				if mode == 1:
					province_name = Dialog().get(message = "Введите название провинции");
					current_added_province = Province();
					current_added_province.set_id(world.get_new_province_id());
					current_added_province.set_name(province_name);
					Log.d(current_added_province.get_name());
					world.addProvince(current_added_province);
				elif mode == 2:
					state_name = Dialog().get(message = "Введите название страны");
					current_added_state = State();
					current_added_state.set_id(world.get_new_state_id());
					current_added_state.set_name(state_name);
					world.addState(current_added_state);
				elif mode == 3:
					pass
				elif mode == 4:
					pass
				continue



		if myrange(1, 4).contains(mode): # mode exit
			try:
				new_mode : int = check_buttons(frameEventsList, right_button_group, mode);
				if mode != new_mode : old_mode = mode; mode = new_mode; raise ModeChangeProcedure;
			except ModeChangeProcedure:
				if mode == 0:
					if old_mode == 1:
						current_added_province = None;
						if len(world.list_of_capitals) != len(world.list_of_province): 
							world.list_of_province.pop(); 
							# Log.d(len(world.list_of_capitals), len(world.list_of_province));
						
					elif old_mode == 2:
						current_added_state = None;
					elif old_mode == 3:
						pass;



		if mode == 1:
			provinceGeometryEdit(frameEventsList,
								current_added_province, 
								mapsurface);
		elif mode == 2:
			addStateMode(frameEventsList, current_added_state, mapsurface);

		elif mode == 3:
			capital_pressed = graphEditMode(frameEventsList, screen, mapsurface, world, capital_pressed);

		elif mode == 4:

			parameter_mode = editParameterMode(frameEventsList, screen, mapsurface, world, parameter_mode);
			# Log.d(parameter_mode);
			world.draw_parameter(screen, mapsurface, parameter_mode);


		menu_blocks_group.draw(screen); # блоки меню отрисовываются при любом режиме работы программы
		right_button_group.draw(screen); # кнопки отрисовываются при любом режиме работы программы.	
		bottom_button_group.draw(screen);
		# if icon_showing: world.icon_draw(screen, mapsurface);
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