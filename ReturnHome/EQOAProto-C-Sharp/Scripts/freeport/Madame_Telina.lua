function  event_say(choice)
diagOptions = {}
    npcDialogue = "What a charming face you have, playerName. I foresee that we will meet again, when I need you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end