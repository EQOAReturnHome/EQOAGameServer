function  event_say(choice)
diagOptions = {}
    npcDialogue = "Time is a waning crescent. Existence is fleeting and soon the light shall be purged. What do you plan to do with your remaining hours of sunlight?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end