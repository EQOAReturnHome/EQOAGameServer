function event_say()
diagOptions = {}
    npcDialogue = "We all have our lots in life and we all need to eat to survive. I can teach you to fish if you so desire."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end