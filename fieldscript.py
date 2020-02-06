import pygame


def coordinates_change(pare, cx, cy, scale):
    pass


# ОРГАНИЗОВАТЬ ФУНКЦИЮ

def runscript(map_file):
    pygame.init()

    running = True
    screen = pygame.display.set_mode((0, 0), pygame.FULLSCREEN)

    fps = 60
    clock = pygame.time.Clock()

    # позанимаемся глиной ага
    CAMERAX = 0
    CAMERAY = 0


    print("Wht`s the problem?")
    while running:
        screen.fill(0, 0, 0)


        for event in pygame.event.get():

            if event.type == pygame.QUIT:
                running = False

        camera_x = CAMERAX
        camera_y = CAMERAY

        clock.tick(fps)
        pygame.display.flip()

    return 0