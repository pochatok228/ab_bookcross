class Verticle:
	def __init__(self, x, y, id):
		self.x = x; self.y = y; self.id;

class Edge:
	def __init__(self, ver1 : Verticle, ver2 : Verticle):
		self.ver_left  = ver1; self.ver_right = ver2;


class Polygon:

	def __init__(self):
		self.verticles = []; self.edges = []; self.current_verticle = 0;

	def add_verticle(self, new_verticle):  # добавление вершины в многоугольник
		self.verticles.append(new_verticle);
		if len(self.verticles) == 2:
			self.edges.append(Edge(self.verticles[0], self.verticles[1]));
		elif len(self.verticles) == 3:
			self.edges.append(Edge(self.verticles[1], self.verticles[2])); 
			self.edges.append(Edge(self.verticles[2], self.verticles[0]));
		else:
			self.edges.pop();
			self.edges.append(Edge(self.verticles[-2], self.verticles[-1]));
			self.edges.append(Edge(self.verticles[-1], self.verticles[0]));

	def checkVerticleIsConvex(self):

		pass

	def checkEdgeIsNested(self):

		pass

	def triangulate(self) -> list: # list <int>;

		list_of_triangles = [];
		while len(self.verticles) != 3:
			if self.current_verticle >= len(self.verticles):
				self.current_verticle = 0;

			if self.checkVerticleIsConvex() and self.checkEdgeIsNested():
				pass



if __name__ == '__main__':
	pass