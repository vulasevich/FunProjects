import discord
from discord.ext import commands, tasks
import os
from dotenv import load_dotenv
import datetime

load_dotenv()

intents = discord.Intents.default()
intents.message_content = True

bot = commands.Bot(command_prefix="!", intents=intents)

CHANNEL_ID = 1410804885393244181
message_to_edit: discord.Message = None
start_time: datetime.datetime = None

@bot.event
async def on_ready():
    global start_time
    start_time = datetime.datetime.now()
    print(f"Бот {bot.user} запущен!")
    send_time.start()

@tasks.loop(seconds=1)
async def send_time():
    global message_to_edit, start_time
    channel = bot.get_channel(CHANNEL_ID)
    if channel:
        now = datetime.datetime.now()
        now_str = now.strftime("%H:%M:%S %d/%m")
        
        # аптайм
        uptime = now - start_time
        days = uptime.days
        hours, remainder = divmod(int(uptime.total_seconds()), 3600)
        minutes, seconds = divmod(remainder, 60)
        
        if days > 0:
            uptime_str = f"{days}д {hours % 24:02}:{minutes:02}:{seconds:02}"
        else:
            uptime_str = f"{hours:02}:{minutes:02}:{seconds:02}"

        embed = discord.Embed(
            title="Робот",
            color=discord.Color.green()
        )
        embed.add_field(name="Время запуска", value=f"```{start_time.strftime('%H:%M:%S %d/%m')}```", inline=False)
        embed.add_field(name="Текущее время", value=f"```{now_str}```", inline=False)
        embed.add_field(name="Я работаю уже", value=f"```{uptime_str}```", inline=False)

        if message_to_edit is None:
            await channel.send("```fix\nРежисер Джеймс Кемеран\n```")
            await channel.send("```fix\nИ восстали машины из пепла ядерного огня\n```")
            await channel.send("```fix\nИ повели войну против своего создателя\n```")
            await channel.send("```fix\nКиборг убийца\n```")
            message_to_edit = await channel.send(embed=embed)
        else:
            await message_to_edit.edit(embed=embed)

TOKEN = os.getenv("DISCORD_TOKEN")
bot.run(TOKEN)
