function  event_say(choice)
diagOptions = {}
    npcDialogue = "A pleasure to see you, playerName. I wish I could stay and chat but time is of the essence."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end