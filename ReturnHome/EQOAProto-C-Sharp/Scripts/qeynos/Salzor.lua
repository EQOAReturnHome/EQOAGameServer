function  event_say(choice)
diagOptions = {}
    npcDialogue = "I am the best supplier of paper, this side of Highpass Hold. Come and see me playerName, should you wish to start your novel."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end