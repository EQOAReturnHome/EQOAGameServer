function event_say()
diagOptions = {}
    npcDialogue = "The Hatebone Orcs have been increasing their knowledge and use of fire. I'm worried that they will carelessly burn down the forest. I pray to Tunare for plenty of rain. If you see it rain playerName, do not curse it. It is a gift to us."
SendDialogue(mySession, npcDialogue, diagOptions)
end