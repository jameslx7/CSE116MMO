import pygame

pygame.init()

win = pygame.display.set_mode((1000, 600))
pygame.display.set_caption("MMO")
clock = pygame.time.Clock()


# Main Loop
run = True
while run:
    clock.tick(27)

    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            run = False

pygame.quit()
