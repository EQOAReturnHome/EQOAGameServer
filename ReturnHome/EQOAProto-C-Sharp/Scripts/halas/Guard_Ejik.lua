function  event_say(choice)
diagOptions = {}
    npcDialogue = "We will NEVER let our enemies set a single FOOT in our great city! FOR HALAS!!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end