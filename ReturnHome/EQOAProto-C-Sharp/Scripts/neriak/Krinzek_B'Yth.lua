function  event_say(choice)
diagOptions = {}
    npcDialogue = "Keep your distance or you'll be swimming with the minnows."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end