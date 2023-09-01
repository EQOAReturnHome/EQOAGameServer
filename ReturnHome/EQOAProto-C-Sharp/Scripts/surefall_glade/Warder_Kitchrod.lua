function  event_say(choice)
diagOptions = {}
    npcDialogue = "Do not be alarmed by the treants, playerName. They are here to help the druids keep the balance of all life."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end