function  event_say(choice)
diagOptions = {}
    npcDialogue = "We're here to make sure Qeynos is clear of those cursed were-beasts. Let us know if you see anything, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end