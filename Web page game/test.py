import pygame
import asyncio 

async def main():
    pygame.init()
    screen = pygame.display.set_mode((800, 600))
    clock = pygame.time.Clock()
    running = True
    
    x,y = 400, 300
    
    vel = 10

    while running:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                running = False

        keys = pygame.key.get_pressed()
      
        if keys[pygame.K_a]:
            x-=vel
        if keys[pygame.K_d]:
            x+=vel
            
        if keys[pygame.K_w]:
            y-=vel
        if keys[pygame.K_s]:
            y+=vel
            
    
        screen.fill("black")
        
        pygame.draw.circle(screen, pygame.Color(200,200,000), (x, y), 50)
        
        pygame.display.flip()

        await asyncio.sleep(0) 
        clock.tick(60)

asyncio.run(main())

