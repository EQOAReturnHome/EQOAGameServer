function event_say()
diagOptions = {}
    npcDialogue = "Do not let your equipment fall into disrepair, playerName. We must be ready for battle at all times. Go visit Blacksmith Silverspear near the city's center whenever you need to correct the wears of combat."
SendDialogue(mySession, npcDialogue, diagOptions)
end