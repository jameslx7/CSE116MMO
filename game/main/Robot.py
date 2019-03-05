import pygame
from base.Character import Character


class Robot(Character):

    images_folder = "static/sprites/robot/"
    walk_images = []

    def __init__(self, x, y):
        super().__init__(x, y, 64, 64, 5)
        self.load_walk_images()
        self.char = self.walk_images[0]
        self.walk_left = [img for img in self.walk_images]
        self.walk_right = [pygame.transform.flip(img, True, False) for img in self.walk_images]

    def load_walk_images(self):
        count = 1
        while count <= 8:
            self.walk_images.append(pygame.transform.scale(pygame.image.load(f"{self.images_folder}Run ({count}).png"), (64, 64)))
            count += 1
        self.walk_images

    def draw(self, win):
        if (self.walk_count+1) >= 24:
            self.walk_count = 0
        if self.left:
            win.blit(self.walk_left[self.walk_count // 3], (self.x, self.y))
            self.walk_count += 1
        elif self.right:
            win.blit(self.walk_right[self.walk_count // 3], (self.x, self.y))
            self.walk_count += 1
        else:
            win.blit(self.char, (self.x, self.y))
