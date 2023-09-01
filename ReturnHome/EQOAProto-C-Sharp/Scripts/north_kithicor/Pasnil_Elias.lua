function event_say()
diagOptions = {}
    npcDialogue = "Time is limited and precious. Learn from my mistakes and spend it with your friends an family. Saren, Jess, and Paul all have their own lives now and I fear that I have decided to retire a bit to late."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end