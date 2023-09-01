function event_say()
diagOptions = {}
    npcDialogue = "We have had many skirmishes with the Hatebone Orcs. Some of them perform noxious rituals to poison the mind, which provides just enough of a distraction, so that one of their thugs may land a crippling blow. Please playerName, keep your distance from them."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end