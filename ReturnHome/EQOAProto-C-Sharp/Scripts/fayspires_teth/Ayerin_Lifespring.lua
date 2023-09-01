function event_say()
diagOptions = {}
    npcDialogue = "One of our duties as druids is to help provide a balance in nature. It's a sad duty, but the wasps nest to the west has gotten out of hand. The wasps there are killing too much of the natural wildlife in the area. We will need to reduce their population to so that the wild  life may thrive again."
SendDialogue(mySession, npcDialogue, diagOptions)
end