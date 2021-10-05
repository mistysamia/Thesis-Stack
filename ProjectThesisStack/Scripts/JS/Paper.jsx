import '~/Content/paper.css';
class Paper extends React.Component {
    render() {
        return (
            
                <article class="eachHomeNotice">
                    <h2 class="text-danger">{this.props.title }</h2>
                    <h5>Published By : Misty chine na eder</h5>
                    <h5>Supervisor : Misty ereo chine na</h5>
                    <article class="d-flex justify-content-between">
                        <p class="fst-italic text-dark">tags: ML,AL,Python</p>
                        <p class="fst-italic  text-success text-end">Published on : May 11,2006</p>
                    </article>
                </article>
          
        );

    }
}


React.render(<Paper title="Basic Python"/>, document.getElementById("paper"));