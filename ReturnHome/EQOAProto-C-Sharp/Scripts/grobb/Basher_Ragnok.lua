function  event_say(choice)
diagOptions = {}
    npcDialogue = "True troll shadowknights must go to small room of visions up this bridge to try to meditate to receive vision from Cazic Thule about their calling."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end