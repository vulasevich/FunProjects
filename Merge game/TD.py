import pygame


pygame.init()
screen = pygame.display.set_mode((1280, 720), pygame.RESIZABLE)
clock = pygame.time.Clock()
running = True
square = 100
towers = []
grabbedTower = 0

class Tower:
    def __init__(self, pos, level, type):
        self.pos = pygame.Vector2(pos)
        self.level = level
        self.type = type


def draw():
    screen.fill((193, 229, 159))
    width, height = screen.get_size()
    for x in range (0,width//square):
        for y in range (0,height//square):
            pygame.draw.rect(screen, (163, 215, 138), ((x*2+y%2)*square, y*square, square, square))
            
    for tower in (towers):
        pygame.draw.circle(screen, (255, 147, 126), tower.pos * square + (square / 2, square / 2), square/2)
    
    if grabbedTower != 0:
        pygame.draw.circle(screen, (255, 147, 126), pygame.mouse.get_pos(), square/2) 
        
    pygame.display.flip()
    
        
    
        
    

while True:
    clock.tick(60)
    draw()
    
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            pygame.quit()
        x,y = pygame.mouse.get_pos() 
        if event.type == pygame.KEYDOWN:
            if event.key == pygame.K_SPACE: #Test
                towers.append(Tower((x//square, y//square), 1,1))
        if event.type == pygame.MOUSEBUTTONDOWN:
            for tower in (towers):
                if tower.pos == pygame.Vector2(x//square,y//square):
                    grabbedTower = tower
                    towers.remove(tower)
            
        if event.type == pygame.MOUSEBUTTONUP:
            if(grabbedTower != 0):
                towers.append(Tower(pygame.Vector2(x // square, y // square), grabbedTower.level, grabbedTower.type))
            grabbedTower = 0
            