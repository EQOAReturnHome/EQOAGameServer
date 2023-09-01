function event_say()
diagOptions = {}
    npcDialogue = "Our family survived the siege on our old home in Takish'Hiz. We were known for our healing water throughout the city. We are fortunate to have made the trek here to Tethelin, where we can continue to offer our services to all elves, and beyond."
SendDialogue(mySession, npcDialogue, diagOptions)
end