function  event_say(choice)
diagOptions = {}
    npcDialogue = "I am here on a private matter from Neriak. I will let you know if there is anything I need from you. You are dismissed."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end