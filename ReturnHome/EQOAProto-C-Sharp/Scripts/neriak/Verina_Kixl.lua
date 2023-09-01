function  event_say(choice)
diagOptions = {}
    npcDialogue = "While I'd love to chat, I have work to be doing. See you around, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end