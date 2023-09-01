function  event_say(choice)
diagOptions = {}
    npcDialogue = "If you're looking for Braver, he can't been seen at the moment. I'm sure you can find your way out now..."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end