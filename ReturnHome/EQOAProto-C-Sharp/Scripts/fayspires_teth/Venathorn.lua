function event_say()
diagOptions = {}
    npcDialogue = "With due diligence, the mastery of alchemy can be very rewarding, as long as the pupil does not disable themselves in the process of using dangerous chemicals. Many young minds have I seen destroyed by a simple moment of carelessness. Don't let this be you, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end