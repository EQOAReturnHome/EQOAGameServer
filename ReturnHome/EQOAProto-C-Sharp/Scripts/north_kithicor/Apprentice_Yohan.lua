function event_say()
diagOptions = {}
    npcDialogue = "Becca complains so much. All of this time and she still hasn't learned the basics of divination."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end