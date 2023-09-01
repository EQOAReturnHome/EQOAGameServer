function event_say()
diagOptions = {}
    npcDialogue = "Follow the road west and you'll reach Highpass Hold. If you travel east, be prepared for a very long journey to Freeport."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end