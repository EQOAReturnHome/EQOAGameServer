function event_say()
diagOptions = {}
    npcDialogue = "As a caster of magic, it is quite important to know your place in battle. If you release all of your powers at once, you may draw the unwanted attention of your foe. Be cautious and time your spells carefully."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end