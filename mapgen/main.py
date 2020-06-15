import pygame
from utils import Log, myrange # logging and range classes
from handlers import Map
from handlers import MenuBlock # right and bottom menu blocks
import sys

current_version = "0.0.1";
resolution = (1000, 900);

mapsurface = None
map_x, map_y = 0, 0;
scale = None
screen = None


def main() -> None:

	global mapsurface, scale, screen

	pygame.init();
	pygame.display.set_caption("MapGen {}".format(current_version));
	screen = pygame.display.set_mode(resolution);

	# main map initialization
	mapgroup = pygame.sprite.Group();
	mapsurface = Map("skyrim_map.jpg", 0, 0, 0);
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

	scale = 0
	pressed = False
	mode = 0;

	"""
		Mode 0: 	is the mode with map showing and nothinhg else

		Mode 1: 	mode when user adds province drawing polygons

		Mode 2: 	mode when user color provinces into countries

		Mode 3: 	mode when user draw a graph

	"""


	# main screen cycle
	while True:

		screen.fill((228, 228, 228))
		if mode == 0:

			# frame render

			# Log.d("Mod zero")

			if pressed:

				up_coords = pygame.mouse.get_pos()
				delta = (up_coords[0] - last_coords[0], up_coords[1] - last_coords[1])
				Map.move(mapsurface, delta)
				last_coords = up_coords

			mapgroup.draw(screen)
			menu_blocks_group.draw(screen)
			# event handling


			for event in pygame.event.get():

				if event.type == pygame.MOUSEBUTTONDOWN and event.button == 2 and mapsurface.collides(event.pos):
					pressed = True
					last_coords = pygame.mouse.get_pos();
					# Log.d("pressed", pressed)

				if event.type == pygame.MOUSEBUTTONUP and event.button == 2:

					pressed = False

				if event.type == pygame.MOUSEBUTTONDOWN and event.button == 4 and mapsurface.collides(event.pos):
					# Log.d("I`ve catch this event!")
					# Log.d("mapsurface.scale = {}".format(mapsurface.scale))
					if myrange(0, 6).contains(mapsurface.scale):
						mapsurface.set_scale(mapsurface.scale + 1)
						mapgroup.update()

				if event.type == pygame.MOUSEBUTTONDOWN and event.button == 5 and mapsurface.collides(event.pos):

					if myrange(1, 7).contains(mapsurface.scale):
						mapsurface.set_scale(mapsurface.scale - 1)
						mapgroup.update()

				if event.type == pygame.QUIT:
					pygame.quit()
					sys.exit()

		pygame.display.flip()








if __name__ == '__main__':
	main()