function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm here looking for clues to some sacred tomes. If you find any, I would like to see them."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end
