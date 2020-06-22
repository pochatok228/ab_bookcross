import time


class ModeChangeProcedure(Exception):

	pass


class Log:

	def i(*msg):

		print(">>> INFO ({}) : {}".format(time.ctime(time.time()), [i for i in msg]));

	def d(*msg):

		print(">>> DEBUG ({}) : {}".format(time.ctime(time.time()), [i for i in msg]));

	def e(*msg):

		print(">>> ERROR ({}) : {}".format(time.ctime(time.time()), [i for i in msg]));

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


	def contains(self, num):
		if num >= self.value_minimum and num <= self.value_maximum:
			return True
		return False