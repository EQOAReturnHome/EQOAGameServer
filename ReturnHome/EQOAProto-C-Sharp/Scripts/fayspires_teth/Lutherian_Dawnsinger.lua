function event_say()
diagOptions = {}
    npcDialogue = "We may deliver rewards to those who are willing to strike down the foes of Tunare's children. When you are ready to serve, Adrwyiel Niteshyre shall guide you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end