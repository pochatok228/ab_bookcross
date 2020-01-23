import pygame


def coordinates_change(pare, cx, cy, scale):
    pass

# ОРГАНИЗОВАТЬ ФУНКЦИЮ


def draw_map(surface_object, map_file, cx, cy, scale):
    with open(map_file) as file:
        file_text = file.read()

    svg_objects = file_text.split('<')
    for svg_object in svg_objects:
        svg_object = svg_object[:-2]
        if 'polygon' in svg_object:
            polygon_angles_cord_string = svg_object.split('=')[-1][1:-2]
        else:
            continue
        pares_of_polygon_angles_cord_string = polygon_angles_cord_string.split()
        angles = []
        for i in range(len(pares_of_polygon_angles_cord_string)):
            angles.append(coordinates_change(tuple(map(float, pares_of_polygon_angles_cord_string[i].split(',')))), cx, cy, scale)
        print(angles)

        # ЗДЕСЬ ПЕРЕДЕЛАТЬ
        # ДОЛЖЕН СОЗДАВАТЬСЯ ОБЪЕКТ КЛАССА ПРОВИНЦИИ С КООРДИНАТАМИ
        # CHANGE COORDINATES ДОЛЖНА НАХОДИТЬСЯ В ФУНКЦИЯХ ОБЪЕКТА ДЛЯ ВЫЗОВА НЕПОСРЕДСТВЕННО ПЕРЕД ОТРИСОВКОЙ
        # ПРОВИНЦИЯ ДОЛЖНА ОТРИСОВЫВАТЬСЯ У СЕБЯ В КЛАССЕ
        # СДЕЛАТЬ КРАСИВУЮ ГРАНИЦУ + ГРАНИЦЫ МЕЖДУ СТРАНАМИ ОТРИСОВАТЬ ОТДЕЛЬНО (НАМНОГО ПОЗЖЕ)
        # СДЕЛАТЬ ПЕРЕТАСКИВАНИЕ И УЧЕЛИЧЕНИЕ КАРТЫ


        pygame.draw.polygon(surface_object, pygame.Color('white'), angles, 1)

def runscript(map_file):
    pygame.init()

    running = True
    screen = pygame.display.set_mode((0, 0), pygame.FULLSCREEN)

    fps = 60
    clock = pygame.time.Clock()

    # позанимаемся глиной ага
    CAMERAX = 0
    CAMERAY = 0

    draw_map(screen, 'maps/arstocka.svg', 0, 0, 1)

    while running:
        # screen.fill(0, 0, 0)

        for event in pygame.event.get():

            if event.type == pygame.QUIT:
                running = False

        camera_x = CAMERAX
        camera_y = CAMERAY

        clock.tick(fps)
        pygame.display.flip()

    return 0