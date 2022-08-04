func detectWeb(w io.Writer, file string) error {
	ctx := context.Background()

	client, err := vision.NewImageAnnotatorClient(ctx)
	if err != nil {
			return err
	}

	f, err := os.Open(file)
	if err != nil {
			return err
	}
	defer f.Close()

	image, err := vision.NewImageFromReader(f)
	if err != nil {
			return err
	}
	web, err := client.DetectWeb(ctx, image, nil)
	if err != nil {
			return err
	}

	fmt.Fprintln(w, "Web properties:")
	if len(web.FullMatchingImages) != 0 {
			fmt.Fprintln(w, "\tFull image matches:")
			for _, full := range web.FullMatchingImages {
					fmt.Fprintf(w, "\t\t%s\n", full.Url)
			}
	}
	if len(web.PagesWithMatchingImages) != 0 {
			fmt.Fprintln(w, "\tPages with this image:")
			for _, page := range web.PagesWithMatchingImages {
					fmt.Fprintf(w, "\t\t%s\n", page.Url)
			}
	}
	if len(web.WebEntities) != 0 {
			fmt.Fprintln(w, "\tEntities:")
			fmt.Fprintln(w, "\t\tEntity\t\tScore\tDescription")
			for _, entity := range web.WebEntities {
					fmt.Fprintf(w, "\t\t%-14s\t%-2.4f\t%s\n", entity.EntityId, entity.Score, entity.Description)
			}
	}
	if len(web.BestGuessLabels) != 0 {
			fmt.Fprintln(w, "\tBest guess labels:")
			for _, label := range web.BestGuessLabels {
					fmt.Fprintf(w, "\t\t%s\n", label.Label)
			}
	}

	return nil
}