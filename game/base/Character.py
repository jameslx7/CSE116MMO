from base.Object import Object


class Character(Object):

    char = None
    walk_right = None
    walk_left = None
    jumping = None
    jump_count = None
    standing = None
    left = None
    right = None
    walk_count = None

    def __init__(self, x, y, w, h, vel=0):
        super().__init__(x, y, w, h, vel)
        self.char = None
        self.walk_right = []
        self.walk_left = []
        self.jumping = False
        self.jump_count = 10
        self.standing = True
        self.left = False
        self.right = False
        self.walk_count = 0
