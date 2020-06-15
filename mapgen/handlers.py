import pygame
from utils import *

class Map(pygame.sprite.Sprite):

	def __init__(self, filename, x, y, scale) -> None:

		pygame.sprite.Sprite.__init__(self)
		self.filename = filename;
		self.scale_dict = {}
		self.scale = scale
		for i in range(8):
			self.scale_dict[i] = (800 + i * 200, 800 + i * 200) # {0 : (800, 800), 1 : (1000, 1000)} etc. generation 
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

	def set_scale(self, scale) -> None:

		self.scale = scale;

	def move(self, delta) -> None:

		self.rect.x += delta[0]
		self.rect.y += delta[1]

	def collides(self, mouseclick_coords) -> bool:

		if myrange(0, 800).contains(mouseclick_coords[0]) and myrange(0, 800).contains(mouseclick_coords[1]):
			return True
		return False



class MenuBlock(pygame.sprite.Sprite):

	def __init__(self, x, y, w, h):
		# Log.d(w, h)
		pygame.sprite.Sprite.__init__(self);
		self.rect = pygame.Rect((x, y, w, h));
		self.image = pygame.Surface((w, h));
		self.image.fill(pygame.Color(35, 35, 53))
		


