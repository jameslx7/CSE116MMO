import pygame


class Object:

    x = None
    y = None
    w = None
    h = None
    vel = None
    __hitbox = None

    def __init__(self, x, y, w, h, vel=0):
        self.x = x
        self.y = y
        self.w = w
        self.h = h
        self.vel = vel

    def draw(self):
        pass

    def move(self):
        pass

    def hitbox(self):
        self.__hitbox = (self.x, self.y, self.w, self.h)
        return self.__hitbox

    pass
