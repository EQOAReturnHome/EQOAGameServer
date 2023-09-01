function  event_say(choice)
diagOptions = {}
    npcDialogue = "A great many seasons I have been a Doomseeker. Many Orcs I have crushed under ma' boot. When that sweet taste of death comes for me, I'll be ready."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end