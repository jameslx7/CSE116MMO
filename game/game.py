import pygame
import base
from main.Robot import Robot


pygame.init()


# setup the window object, giving the width and height of the game window as arguments
win = pygame.display.set_mode((1000, 600))
# set the title of the game window
pygame.display.set_caption("MMO")
# get the clock object for use in the main loop
clock = pygame.time.Clock()


bg = pygame.image.load("static/images/Background.png")


robot = Robot(100, 500)


def redraw_game_window():
    win.blit(bg, (0, 0))
    robot.draw(win)
    pygame.display.update()


# Main Loop
run = True
while run:
    # framerate set, set to 27fps
    clock.tick(27)

    # event checking
    for event in pygame.event.get():
        # on quit event (user exiting the game window), set run to False to end Main Loop
        if event.type == pygame.QUIT:
            run = False

    keys = pygame.key.get_pressed()

    redraw_game_window()


# quit the game
pygame.quit()
