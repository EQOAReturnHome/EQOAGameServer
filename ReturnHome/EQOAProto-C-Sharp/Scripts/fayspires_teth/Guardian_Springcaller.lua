function event_say()
diagOptions = {}
    npcDialogue = "Here, you may learn to clear your mind and train. Make every blow count. Make faithful your heart to the Mother of All, and may she guide you to strike down all evil."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end