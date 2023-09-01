function  event_say(choice)
diagOptions = {}
    npcDialogue = "Grandma has always acted a bit�perculiar...when Donpo comes around. Ah well, I asked her first and we've been married these past 53 years. I'll never tell her that I changed her recipes to my mothers. She was an awful cook when we first married."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end