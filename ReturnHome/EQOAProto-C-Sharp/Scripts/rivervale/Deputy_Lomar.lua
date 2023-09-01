function  event_say(choice)
diagOptions = {}
    npcDialogue = "Just south of this tower is one of the tunnels that leads into Rivervale. Beyond that is the Inn and Coach."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end