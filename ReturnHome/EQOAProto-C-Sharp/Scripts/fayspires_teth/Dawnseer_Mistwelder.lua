function event_say()
diagOptions = {}
    npcDialogue = "Our number has dwindled since the great burning. We must seek a new beginning before we are lost in the twilight."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end