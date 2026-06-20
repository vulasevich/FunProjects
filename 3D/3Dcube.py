# Example file showing a basic pygame "game loop"
import pygame
from pygame import Vector2
from pygame import draw
from math import cos
from math import sin
from math import radians
import numpy as np

# pygame setup
pygame.init()
screen = pygame.display.set_mode((1280, 720)) #2**7 2**3*9
clock = pygame.time.Clock()
running = True


length = 100
posx=0
posy=0
posz=200
lengthsqrt = (length**2 + length**2)**0.5 

def drawLine(p1,p2,color):
    cx = int(screen.width/2)
    cy = int(screen.height/2)
    center = pygame.Vector2(cx,cy)
    draw.line(screen, color, center+p1,center+p2)

def getPoint(x,y,z):
    
    angle=radians(angle_between((0,0),(x,z)))*2.5
    X = posx + cos(angle+pygame.time.get_ticks()/1000) * lengthsqrt 
    Z = posz + sin(angle+pygame.time.get_ticks()/1000) * lengthsqrt
    
    dist = (100/Z)**0.5
    print(dist)
    return(Vector2(X*dist,y*dist))



def angle_between(p1, p2):
    ang1 = np.arctan2(*p1[::-1])
    ang2 = np.arctan2(*p2[::-1])
    return np.rad2deg((ang1 - ang2) % (2 * np.pi))

while running:
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False

    screen.fill("white")

    
    #for x in range(1, 16): #1280/80
    #    draw.line(screen, "black", Vector2(x*80, 0), Vector2(x*80, 720))
    #for y in range(0, 9): #720/80
    #    draw.line(screen, "black", Vector2(0, y*80), Vector2(1280, y*80))
        

    
    
    #       brt_______________blt
    #         /|             /|
    #        / |            / |
    #       /  |           /  |
    #   frt/___|__________/flt|
    #      |   |          |   |
    #      |brb|__________|___|blb
    #      |   /          |   /
    #      |  /           |  /
    #      | /            | /
    #   frb|/_____________|/flb
    #       

    
    

    brt=getPoint(posx - length, posy + length, posz - length) #-+-
    blt=getPoint(posx + length, posy + length, posz - length) #++-
    frt=getPoint(posx - length, posy + length, posz + length) #-++
    flt=getPoint(posx + length, posy + length, posz + length) #+++
    
    brb=getPoint(posx - length, posy - length, posz - length) #---
    blb=getPoint(posx + length, posy - length, posz - length) #+--
    frb=getPoint(posx - length, posy - length, posz + length) #--+
    flb=getPoint(posx + length, posy - length, posz + length) #+-+
    
     
     
    
    drawLine(brt, blt, "blue")
    drawLine(brt, frt, "blue")
    drawLine(blt, flt, "blue")
    drawLine(frt, flt, "blue")
    
    drawLine(brt, flt, "blue")
    drawLine(frt, blt, "blue")


    drawLine(brb, blb, "green")
    drawLine(brb, frb, "green")
    drawLine(blb, flb, "green")
    drawLine(frb, flb, "green")
    
    drawLine(brb, flb, "green")
    drawLine(frb, blb, "green")
    
    
    drawLine(brt, brb, "red")
    drawLine(blt, blb, "red")
    drawLine(frt, frb, "red")
    drawLine(flt, flb, "red")

    
    
    pygame.display.flip()
    clock.tick(60) 

pygame.quit()