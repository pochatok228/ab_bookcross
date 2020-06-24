import pygame
from utils import *

class Map(pygame.sprite.Sprite):

	def __init__(self, filename : str, x : int, y : int, scale : int) -> None:

		pygame.sprite.Sprite.__init__(self)
		self.filename = filename;
		self.scale_dict = {};
		self.scale = scale
		for i in range(8):
			self.scale_dict[i] = (800 + i * 200, 800 + i * 200) # {0 : (800, 800), 1 : (1000, 1000) ... 7(2200, 2200)} etc. generation 
		self.image_original = pygame.image.load(self.filename)

		Log.expects(self.scale_dict[self.scale], (800, 800))
		self.image = pygame.transform.scale(self.image_original, self.scale_dict[self.scale]);

		Log.expects(self.image.get_rect().size, (800, 800))
		# Log.d(self.image.get_rect().size);
		self.rect = self.image.get_rect();
		# self.rect.x = x
		# self.rect.y = y
		
		# Log.d(self.rect.x, self.rect.y)

	def update(self) -> None:

		self.image = pygame.transform.scale(self.image_original,  self.scale_dict[self.scale])

	def set_scale(self, scale : int) -> None:

		self.scale = scale;

	def get_scale(self) -> int:

		return self.scale;

	def get_coords(self) -> tuple:

		return self.rect.x, self.rect.y;

	def move(self, delta : tuple) -> None: #tuple <int>

		self.rect.x += delta[0]
		self.rect.y += delta[1]

	def collides(self, mouseclick_coords : tuple) -> bool: # tuple <int>

		if myrange(0, 800).contains(mouseclick_coords[0]) and myrange(0, 800).contains(mouseclick_coords[1]):
			return True
		return False



class MenuBlock(pygame.sprite.Sprite):

	def __init__(self, x : int, y : int, w : int, h : int) -> None:
		# Log.d(w, h)
		pygame.sprite.Sprite.__init__(self);
		self.rect = pygame.Rect((x, y, w, h));
		self.image = pygame.Surface((w, h));
		self.image.fill(pygame.Color(35, 35, 35))
		


class Button(pygame.sprite.Sprite):


	def __init__(self, x : int, y : int, w : int, h : int, text : str, mode : int) -> None:

		pygame.sprite.Sprite.__init__(self);
		# pass

		self.mode : int = mode;

		
		self.rect = pygame.Rect((x, y, w, h));
		self.image = pygame.Surface((w, h));
		self.image.fill(pygame.Color(28, 28, 28));


		self.active_color = (228, 228, 228);
		self.passive_color = (100, 100, 100);
		self.pressed_color = (0, 200, 10)

		self.color = self.active_color;
		self.text = text;
		self.font = pygame.font.SysFont('arial', 18);
		self.text_render = self.font.render(self.text, 1, self.color);

		self.image.blit(self.text_render, (10, 25));


	def activate(self) -> None:
		self.color = self.active_color;
		self.update();

	def deactivate(self) -> None:
		self.color = self.passive_color;
		self.update();

	def press(self) -> None:
		self.color = self.pressed_color;
		self.update();

	def update(self) -> None:
		self.text_render = self.font.render(self.text, 1, self.color);
		self.image.blit(self.text_render, (10, 25));

	def collides(self, mouseclick_coords : tuple) -> bool: # tuple of int
		x1 = self.rect.x;
		x2 = self.rect.x + self.rect.w;
		y1 = self.rect.y;
		y2 = self.rect.y + self.rect.h;

		if myrange(x1, x2).contains(mouseclick_coords[0]) and myrange(y1, y2).contains(mouseclick_coords[1]):
			return True;
		return False;

	def is_able_to_press(self) -> bool:
		if self.color == self.active_color or self.color == self.pressed_color: return True; return False;