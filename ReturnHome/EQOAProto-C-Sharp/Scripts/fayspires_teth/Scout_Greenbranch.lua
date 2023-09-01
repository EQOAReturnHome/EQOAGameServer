function event_say()
diagOptions = {}
    npcDialogue = "Beware the orc camps just to the south of Tethelin over the mountain. I've just returned from there, and I am sad to report their increase in numbers."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end