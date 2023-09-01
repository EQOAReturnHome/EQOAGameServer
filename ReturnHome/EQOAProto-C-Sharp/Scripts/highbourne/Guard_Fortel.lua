function event_say()
diagOptions = {}
    npcDialogue = "Welcome to Highbourne. Make sure to show your visa to Ijan if you plan to remain within the city walls for more than two days."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end